﻿namespace WebAppLib1.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Prestamo> Prestamos { get; set; }
    }
}