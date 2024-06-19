namespace WebAppLib1.Models
{
    /// <summary>
    /// Clase ApiResponse que define la estructura de las respuestas del API.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Indica si la operacion fue exitosa.
        /// </summary>
        public required bool IsSuccess { get; set; }

        /// <summary>
        /// Mensaje asociado a la operacion ejecutada.
        /// </summary>
        public required string ResultMessage { get; set; }

        /// <summary>
        /// Objeto asociado a la operacion ejecutada.
        /// </summary>
        public object? ResultObject { get; set; }

        /// <summary>
        /// Si IsSuccess es false, aqui se incluira el stack completo del error.
        /// </summary>
        public string? ErrorMessage { get; set; }

    }
}
