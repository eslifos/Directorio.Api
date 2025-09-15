using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Directorio.WpfClient.ViewModels
{

    // Reutiliza o crea un modelo similar al del API
    public class PersonaModel
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Identificacion { get; set; }
    }


    public partial class MainViewModel : ObservableValidator // ObservableValidator nos da validación y notificación
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [NotifyDataErrorInfo]
        private string _nombre;

        [ObservableProperty]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [NotifyDataErrorInfo]
        private string _apellidoPaterno;

        [ObservableProperty]
        private string _apellidoMaterno;

        [ObservableProperty]
        [Required(ErrorMessage = "La identificación es obligatoria")]
        [NotifyDataErrorInfo]
        private string _identificacion;

        public MainViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri("https://localhost:7200") };
        }

        [RelayCommand]
        private async Task Registrar()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                MessageBox.Show("Por favor, corrige los errores antes de continuar.", "Datos incompletos");
                return;
            }

            var nuevaPersona = new PersonaModel
            {
                Nombre = Nombre,
                ApellidoPaterno = ApellidoPaterno,
                ApellidoMaterno = ApellidoMaterno,
                Identificacion = Identificacion
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Directorio", nuevaPersona);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("¡Usuario registrado con éxito!", "Éxito");
                    Nombre = string.Empty;
                    ApellidoPaterno = string.Empty;
                    ApellidoMaterno = string.Empty;
                    Identificacion = string.Empty;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al registrar: {error}", "Error de API");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"No se pudo conectar con el servidor. Asegúrate de que la API esté en ejecución.\n\nError: {ex.Message}", "Error de Conexión");
            }
        }
    }
}