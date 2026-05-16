using Microsoft.Win32;
using SportsBooking.Models.DTO;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace SportsBooking.Wpf;

public partial class MainWindow : Window
{
    private readonly HttpClient _httpClient = new();
    private string? _jwtToken;

    public MainWindow()
    {
        InitializeComponent();
        _httpClient.BaseAddress = new Uri("https://localhost:7260/");
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var dto = new LoginDto
        {
            Email = EmailTextBox.Text,
            Password = PasswordBox.Password
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", dto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                _jwtToken = result?.Token;
                ResultTextBlock.Text = "Успешный вход! Токен сохранён.";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ResultTextBlock.Text = "Неверный email или пароль";
            }
            else
            {
                ResultTextBlock.Text = $"Ошибка входа: {(int)response.StatusCode}";
            }
        }
        catch
        {
            ResultTextBlock.Text = "Ошибка подключения к API. Проверьте, запущен ли сервер.";
        }
    }

    private async void LoadGroundsButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var grounds = await _httpClient.GetFromJsonAsync<List<SportsBooking.Models.SportsGround>>("api/SportsGrounds");

            GroundsListBox.Items.Clear();

            if (grounds != null)
            {
                foreach (var ground in grounds)
                {
                    GroundsListBox.Items.Add($"{ground.Name} — {ground.Address} — {ground.PricePerHour} ₽/час");
                }
            }
        }
        catch
        {
            MessageBox.Show("Ошибка загрузки площадок");
        }
    }

    private async void BookButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var booking = new
            {
                id = 0,
                userId = 1,
                sportsGroundId = 1,
                startTime = DateTime.Now.AddDays(1),
                endTime = DateTime.Now.AddDays(1).AddHours(1),
                statusId = 1
            };

            var response = await _httpClient.PostAsJsonAsync("api/Bookings", booking);

            if (response.IsSuccessStatusCode)
                MessageBox.Show("Площадка успешно забронирована!");
            else
                MessageBox.Show("Ошибка бронирования");
        }
        catch
        {
            MessageBox.Show("Ошибка подключения к API");
        }
    }

    private async void UploadFileButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
        };

        if (dialog.ShowDialog() != true)
            return;

        try
        {
            using var form = new MultipartFormDataContent();
            using var fileStream = File.OpenRead(dialog.FileName);
            using var fileContent = new StreamContent(fileStream);

            form.Add(fileContent, "file", Path.GetFileName(dialog.FileName));

            var response = await _httpClient.PostAsync("api/Files/upload", form);

            if (response.IsSuccessStatusCode)
                MessageBox.Show("Файл успешно загружен");
            else
                MessageBox.Show("Ошибка загрузки файла");
        }
        catch
        {
            MessageBox.Show("Ошибка подключения к API");
        }
    }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
}