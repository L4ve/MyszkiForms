using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyszkiForms
{
    public partial class Form1 : Form
    {
        private List<Mouse> mice;

        public Form1()
        {
            InitializeComponent();
            LoadMice();
        }

        private void LoadMice()
        {
            // lista myszek (chciałem zrobić używając bazy z sqllite w maui, ale kod mi padł z 5 razy więc sie poddałem
            mice = new List<Mouse>
            {
                new Mouse { Model = "OP1 8K", Firma = "ENDGAMEGEAR", Sensor = "PixArt 3389", Waga = 80, Link = "https://www.amazon.com/stores/ENDGAMEGEAR/page/D37B2C1B-5C1F-4974-8EE1-FEF5A7E567C0" },
                new Mouse { Model = "DeathAdder V3", Firma = "Razer", Sensor = "Focus+", Waga = 82, Link = "https://www.amazon.com/DeathAdder-Wired-Gaming-Mouse-HyperPolling/dp/B0B6XTDJS1" },
                new Mouse { Model = "Hitscan HyperLight 8K", Firma = "Hitscan", Sensor = "PixArt PAW3395", Waga = 39, Link = "https://hitscan.com/products/hyperlight" },
                new Mouse { Model = "Logitech G102", Firma = "Logitech", Sensor = "Logitech Mercury", Waga = 85, Link = "https://www.logitechg.com/en-ph/products/gaming-mice/g102-lightsync-rgb-gaming-mouse.html" },
                new Mouse { Model = "Logitech G Pro X Superlight 2", Firma = "Logitech", Sensor = "HERO 2", Waga = 53, Link = "https://www.logitechg.com/en-us/products/gaming-mice/pro-x-superlight-2.html" },
                new Mouse { Model = "Darmoshark M5", Firma = "Darmoshark", Sensor = "PixArt PAW3950", Waga = 31, Link = "https://darmoshark.com/products/darmoshark-m5-air-mp-fish-head-trio-mode-game-mouse" },
                new Mouse { Model = "G-Wolves Lycan 8K", Firma = "G-Wolves", Sensor = "PixArt PAW3950", Waga = 27, Link = "https://shop.g-wolves.com/products/lycan-8k-wireless-mouse" },
                new Mouse { Model = "SteelSeries Aerox 3", Firma = "SteelSeries", Sensor = "TrueMove Core", Waga = 68, Link = "https://steelseries.com/gaming-mice/aerox-3" },
                new Mouse { Model = "Lamzu Maya X", Firma = "Lamzu", Sensor = "PixArt PAW3950", Waga = 47, Link = "https://lamzu.com/products/lamzu-maya-x" }
            };

            comboBox1.DataSource = mice.ToList();
            comboBox2.DataSource = mice.ToList();

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;

            comboBox1.SelectedIndex = 2;
            comboBox2.SelectedIndex = 1;

            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            linkLabel2.LinkClicked += LinkLabel2_LinkClicked;
        }

        // Myszka1 pokazane dane
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is Mouse mouse)
            {
                Myszka1_model.Text = $"Model: {mouse.Model}";
                Myszka1_Firma.Text = $"Firma: {mouse.Firma}";
                Myszka1_Sensor.Text = $"Sensor: {mouse.Sensor}";
                Myszka1_weight.Text = $"Waga(g): {mouse.Waga}";
                linkLabel1.Text = "Link";
            }

            // porównanie wag i podświetlenie lżejszej myszki
            HighlightMouse();
        }

        // Myszka2 pokazane dane
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem is Mouse mouse)
            {
                Myszka2_model.Text = $"Model: {mouse.Model}";
                Myszka2_firma.Text = $"Firma: {mouse.Firma}";
                Myszka2_Sensor.Text = $"Sensor: {mouse.Sensor}";
                Myszka2_Waga.Text = $"Waga(g): {mouse.Waga}";
                linkLabel2.Text = "Link";
            }

            // porównanie wag i podświetlenie lżejszej myszki
            HighlightMouse();
        }

        // sprawdza która myszka jest lżejsza i ustawia zielony kolor
        // sprawdza która myszka jest lżejsza i ustawia zielony + pogrubienie
        private void HighlightMouse()
        {
            if (comboBox1.SelectedItem is not Mouse m1 ||
                comboBox2.SelectedItem is not Mouse m2)
                return;

            // reset kolorów i czcionki
            SetMouse1Color(Color.Black, false);
            SetMouse2Color(Color.Black, false);

            if (m1.Waga < m2.Waga)
                SetMouse1Color(Color.Green, true);
            else if (m2.Waga < m1.Waga)
                SetMouse2Color(Color.Green, true);
        }


        // ustawianie koloru etykiet dla myszki 1
        // ustawianie koloru tylko dla wagi myszki 1
        private void SetMouse1Color(Color color, bool bold)
        {
            Myszka1_weight.ForeColor = color;
            Myszka1_weight.Font = new Font(
                Myszka1_weight.Font,
                bold ? FontStyle.Bold : FontStyle.Regular
            );
        }

        // ustawianie koloru tylko dla wagi myszki 2
        private void SetMouse2Color(Color color, bool bold)
        {
            Myszka2_Waga.ForeColor = color;
            Myszka2_Waga.Font = new Font(
                Myszka2_Waga.Font,
                bold ? FontStyle.Bold : FontStyle.Regular
            );
        }

        //działanie linku w myszka1 (UseShellExecute powoduje że link odpala sie w domyślnej przeglądarce zamiast w stronie)
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedItem is Mouse mouse)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = mouse.Link,
                    UseShellExecute = true
                });
            }
        }

        //działanie linku w myszka2 (UseShellExecute powoduje że link odpala sie w domyślnej przeglądarce zamiast w stronie)
        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox2.SelectedItem is Mouse mouse)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = mouse.Link,
                    UseShellExecute = true
                });
            }
        }
    }

    // Database
    class Mouse
    {
        public string Model { get; set; }
        public string Firma { get; set; }
        public string Sensor { get; set; }
        public int Waga { get; set; }
        public string Link { get; set; }

        public override string ToString() => Model;
    }
}
