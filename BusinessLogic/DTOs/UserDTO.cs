namespace BusinessLogic.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public RoleEnum Role { get; set; }
    }

    public enum RoleEnum
    {
        /// <summary>
        /// Адміністратор системи (повний обсяг прав)
        /// </summary>
        Admin,

        /// <summary>
        /// Зареєстрований користувач
        /// </summary>
        RegisteredUser,

        /// <summary>
        /// Редактор
        /// </summary>
        Editor,

        /// <summary>
        /// Гість
        /// </summary>
        Guest
    }
}
