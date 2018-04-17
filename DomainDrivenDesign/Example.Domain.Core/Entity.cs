using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Example.Domain.Core;

namespace Example.Domain.Core
{
    /// <summary>
    /// Clase que representa una Entidad del Dominio
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Entity<T> where T : Entity<T>
    {
        /// <summary>
        /// "Alias" for Func
        /// </summary>
        /// <param name="repository">the repository</param>
        /// <returns></returns>
        public delegate ValidationList EntityValidator(IRepository<T> repository); // Func<IRepository<T>, ValidationList>

        #region Propiedades

        /// <summary>
        /// Identificador unívoco.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        #endregion

        #region Validaciones

        /// <summary>
        /// Devuelve un EntityValidator con la validación a usar antes de añadir
        /// </summary>
        /// <returns>El validator</returns>
        public virtual EntityValidator GetAddValidator() => (repository) => new ValidationList();

        /// <summary>
        /// Devuelve un EntityValidator con la validación a usar antes de actualizar
        /// </summary>
        /// <returns>El validator</returns>
        public virtual EntityValidator GetUpdateValidator() => (repository) => new ValidationList();

        /// <summary>
        /// Devuelve un EntityValidator con la validación a usar antes de borrar
        /// </summary>
        /// <returns>El validator</returns>
        public virtual EntityValidator GetRemoveValidator() => (repository) => new ValidationList();

        #endregion
    }
}
