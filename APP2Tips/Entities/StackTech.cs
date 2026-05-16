using System;
using System.Collections.Generic;
using System.Text;

namespace APP2Tips.Entities
{
    /// <summary>
    /// Es la entidad u objeto DTO (Data Transfer Object) que representa un Stack Tecnológico en la aplicación.
    /// </summary>
    internal class StackTech
    {
        /// <summary>
        /// ID numérico autoincremental para identificar cada stack tecnológico. 
        /// Este campo es la clave primaria de la tabla y se utiliza para establecer 
        /// relaciones con otras tablas, como la tabla de tips, donde se referencia el 
        /// ID del stack tecnológico al que pertenece cada tip.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Este es el nombre del Stack Tecnológico, como "C#", "JavaScript", "Python", etc. 
        /// Este campo es esencial para identificar y categorizar los tips según la tecnología a 
        /// la que se refieren. Es un campo de texto que debe ser único para evitar confusiones entre 
        /// diferentes stacks tecnológicos.
        /// </summary>
        public string NombreStack { get; set; }

        /// <summary>
        /// Este es el campo de descripción del Stack Tecnológico, donde se puede proporcionar 
        /// información adicional sobre el stack, como su propósito, características principales, 
        /// o cualquier detalle relevante que ayude a los usuarios a entender mejor el contexto de 
        /// los tips asociados a ese stack tecnológico. Este campo es opcional pero puede ser muy útil
        /// para ofrecer una visión más completa del stack tecnológico.
        /// </summary>
        public string DescripcionStack { get; set; }
    }
}
