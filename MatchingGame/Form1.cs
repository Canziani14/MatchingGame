using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // Utilice este objeto aleatorio para elegir iconos aleatorios para los cuadrados.
        Random random = new Random();
        // Cada una de estas letras es un ícono en la fuente Webdings y cada icono aparece dos veces en esta lista
        List<string> icons = new List<string>()
                {
                 "!", "!", "N", "N", ",", ",", "k", "k",
                 "b", "b", "v", "v", "w", "w", "z", "z"
                };

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();

        }



        // firstClicked apunta al primer control Etiqueta en el que hace clic el jugador, pero será nulo
        //si el jugador aún no ha hecho clic en una etiqueta
        Label firstClicked = null;
        //secondClicked apunta al segundo control Etiqueta en el que el jugador hace clic  
        Label secondClicked = null;


        /// <summary>
        /// Asigne cada ícono de la lista de íconos a un cuadrado aleatorio
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AssignIconsToSquares()
        {

            // El TableLayoutPanel tiene 16 etiquetas,
            //y la lista de íconos tiene 16 íconos,
            //por lo que se extrae un ícono al azar de la lista y se agrega a cada etiqueta
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }


            }
        }

        /// <summary>
        /// El evento Click de cada etiqueta es manejado por este controlador de eventos
        /// </summary>
        /// <param name="sender">The label that was clicked</param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            // // El cronómetro solo se activa después de que se le hayan mostrado al jugador dos íconos
            // que no coinciden, así que ignore cualquier clic si el cronómetro está funcionando
            if (timer1.Enabled == true)
                return;
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                // Si la etiqueta en la que se hizo clic es negra, el jugador hizo clic en un ícono
                // que ya ha sido revelado; ignora el clic
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                //// Si firstClicked es nulo, este es el primer icono del par en el que el jugador hizo clic, así que
                ///establezca firstClicked en la etiqueta en la que el jugador hizo clic, cambie
                ///su color a negro y regrese
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                // Si el jugador llega hasta aquí, el cronómetro no se está ejecutando y firstClicked
                // no es nulo, por lo que este debe ser el segundo icono en el que el jugador hizo clic.
                // Establece su color en negro.
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // verifica si el usuario gano
                CheckForWinner();


                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                //Si el jugador llega hasta aquí, hizo clic en dos íconos diferentes, así que inicie
                //el cronómetro (que esperará tres cuartos de tiempo).
                //un segundo y luego ocultar los iconos)
                timer1.Start();
            }
        }

            /// <summary>
            ///Este cronómetro se inicia cuando el jugador hace clic
            ///dos íconos que no coinciden,
            ///entonces cuenta tres cuartos de segundo y luego se apaga y oculta ambos íconos
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void timer1_Tick(object sender, EventArgs e)
            {
                // Frena el tiempo
                timer1.Stop();
                // Ocultar ambos iconos
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;
            // Restablecer el primer clic y el segundo clic
            //así que la próxima vez que se coloque una etiqueta
            //Cuando se hace clic, el programa sabe que es el primer clic.
                firstClicked = null;
                secondClicked = null;
            }



        /// <summary>
        ///Verifique cada ícono para ver si coincide, haciendo
        ///comparando su color de primer plano con su color de fondo.
        ///Si todos los íconos coinciden, el jugador gana
        /// </summary>
        private void CheckForWinner()
        {
            // Revise todas las etiquetas en TableLayoutPanel y verifique cada una para ver si su ícono coincide.
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            //Si el bucle no regresó, no encontró ningún ícono no coincidente.
            //Eso significa que el usuario ganó. Mostrar un mensaje y cerrar el formulario.
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }


    }
}
