using System;
using System.Windows.Forms;
using Maxx53.Games;

namespace SnakeTheGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Объявляем эземпляр класса
        SnakeGame snake;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Для предотвращения мерцания при перерисовке
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            //Создаем экземпляр с игрой на форме.
            snake = new SnakeGame(this);

            //Добавляем событие на нажатие Esc
            snake.PressEsc += new SnakeGame.EscPressHandler(snake_PressEsc);

            UpdateLevelBox();

        }


        private void UpdateLevelBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Случайный");

            for (int i = 0; i < snake.levels.Count; i++)
            {
                comboBox1.Items.Add(System.IO.Path.GetFileName(snake.levels[i]));
            }

            if (snake.levels.Count != 0)
                comboBox1.SelectedIndex = 0;
        }

        private void SetRecommendedSize()
        {
            //Рекомендуемый размер
            if (checkBox2.Checked)
            {
                //Задаем размер, центрируем форму
                this.ClientSize = new System.Drawing.Size(800, 600);
                this.WindowState = FormWindowState.Normal;
                this.CenterToScreen();
            }
        }

        //Обрабатываем событие
        private void snake_PressEsc(object sg, EventArgs e)
        {
            UpdateLevelBox();

            //Показываем панель с контролами
            panel1.Visible = true;

            //Центрируем панель
            CenterPanel();

            //Перерисовываем форму
            this.Invalidate();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Прячем контролы
            panel1.Visible = false;

            //Фокусируемся на форме
            this.Focus();

            //Выставляем скорость игры
            snake.GameSpeed = (int)numericUpDown2.Value;

            //Выставляем количество яблок на поле
            snake.AppleCount = (int)numericUpDown3.Value;

            //Выставляем количество куриц на поле
            snake.ChickCount = (int)numericUpDown4.Value;

            //Проигрывать звуковые эффекты
            snake.Mute = checkBox1.Checked;

            SetRecommendedSize();

            //Запускаем игру с номером левела
            //Ноль для случайного левела
            snake.StartNewGame(comboBox1.SelectedIndex - 1);

        }

        private void CenterPanel()
        {
            //Если панель видна, значит мы в главном меню
            if (panel1.Visible)
            {
                //Центрируем панель
                panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
                panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Прячем контролы
            panel1.Visible = false;

            //Фокусируемся на форме
            this.Focus();

            SetRecommendedSize();

            snake.RunEditor(comboBox1.SelectedIndex - 1, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Управление\r\nВ игре:\r\n\r\nWASD или Стрелки" +
                " - изменить направление движения змейки;" +
                "\r\nSpace (пробел) - ускорение;" +
                "\r\nR - перезапуск текущего уровня;" +
                "\r\nEsc - вернуться в главное меню;" +
                "\r\nP - пауза;" +
                "\r\nE - переключиться на редактирование текущего уровня." +
                "\r\nВ редакторе:\r\n\r\nЛевая кнопка мыши - поставить камень в позицию курсора;" +
                "\r\nПравая кнопка мыши - удалить камень из позиции курсора;" +
                "\r\nСредняя кнопка мыши (колесо) - поставить змейку в позицию курсора, повернуть змейку;" +
                "\r\nCtrl+X - очистить игровое поле от камней;" +
                "\r\nE - запустить игру с текущим уровнем;" +
                "\r\nCtrl+S - сохранить уровень в файл;" +
                "\r\nCtrl+O - открыть уровень из файла.");
        }
    }
}
