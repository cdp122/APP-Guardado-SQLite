using System;
using System.Collections.Generic;
using System.Text;

namespace APP2Tips.Entities
{
    /// <summary>
    /// Esta es la entidad u objeto DTO (Data Transfer Object) que representa un Tip en la aplicación.
    /// </summary>
    internal class Tip
    {
        /// <summary>
        /// ID único del TIP, que se utiliza para identificar de manera unívoca cada tip en la base de datos.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Este es el ID del Stack Tecnológico al que pertenece el tip. Es un campo numérico que actúa como clave foránea.
        /// </summary>
        public int StackTechID { get; set; }

        /// <summary>
        /// Este es el título del tip, que debe ser breve y descriptivo para que los usuarios 
        /// puedan entender rápidamente de qué trata el tip. Es un campo de texto que se muestra 
        /// en la lista de tips y en los detalles de cada tip.
        /// </summary>
        public string TituloTip { get; set; }

        /// <summary>
        /// Esta es una descripción más detallada del tip, donde se pueden proporcionar instrucciones, 
        /// consejos, ejemplos de código, o cualquier información relevante que ayude a los usuarios a 
        /// comprender y aplicar el tip de manera efectiva. Este campo es esencial para que los tips sean 
        /// útiles y prácticos para los usuarios.
        /// </summary>
        public string DescripcionTip { get; set; }

        /// <summary>
        /// Este es directamente el tip. Aquí se pone el código o comando del Stack Tecnológico al que 
        /// se refiere el tip. Este campo es crucial, ya que es el contenido principal del tip, 
        /// y debe ser claro y preciso para que los usuarios puedan copiarlo o entenderlo fácilmente.
        /// </summary>
        public string CodigoTip { get; set; }
    }
}
