
using BoardNesting.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoardNesting
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl, INotifyPropertyChanged
    {
        #region Interfaces Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChangedProperty(String property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion

        [Description("Backgroung Color of Board"), Category("Colors"), DisplayName("BoardBackgroundColor")]
        public Brush BoardBackgroundColor { get { return (Brush)GetValue(BoardBackgroundColorProperty); } set { SetValue(BoardBackgroundColorProperty, value); } }
        public static readonly DependencyProperty BoardBackgroundColorProperty =
            DependencyProperty.Register("BoardBackgroundColor", typeof(Brush), typeof(Board), new PropertyMetadata(new SolidColorBrush(Colors.Green)));

        public Board()
        {
            InitializeComponent();
            Graph();
        }

        public BoardBase Data { get; set; }
        int lineCount;
        private void Graph()
        {
            Data = new BoardBase(1000, 550);

            Grid BodyGrid = new Grid();
            BodyGrid.Width = Data.Width;
            BodyGrid.Height = Data.Height;
            BodyGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            BodyGrid.VerticalAlignment = VerticalAlignment.Stretch;
            BodyGrid.ShowGridLines = true;
            BodyGrid.Background = new SolidColorBrush(Colors.BurlyWood);

            lineCount = Data.Pieces.GroupBy(piece => piece.Line).Count();

            var bigPiece = from item in Data.Pieces
                           group item by item.Line into releaseGroup
                           select new { ReleaseNumber = releaseGroup.Key, Height = releaseGroup.Select(o => o.Height).Max() };

            #region Top Row
            RowDefinition topRow = new RowDefinition();
            BodyGrid.RowDefinitions.Add(topRow);
            StackPanel topStack = new StackPanel();
            Grid.SetColumn(topStack, 0);
            Grid.SetRow(topStack, 0);
            BodyGrid.Children.Add(topStack);
            #endregion

            #region Horizontal Cuts
            RowDefinition[] gridRowArr = new RowDefinition[lineCount];
            StackPanel[] stackRowArr = new StackPanel[lineCount];
            var i = 0;
            foreach (var item in bigPiece.Reverse())
            {
                gridRowArr[i] = new RowDefinition();
                gridRowArr[i].Height = new GridLength(item.Height, GridUnitType.Pixel);
                BodyGrid.RowDefinitions.Add(gridRowArr[i]);

                stackRowArr[i] = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    //Background = new SolidColorBrush(Colors.Aqua),
                };
                Grid.SetColumn(stackRowArr[i], 0);
                Grid.SetRow(stackRowArr[i], i + 1);
                BodyGrid.Children.Add(stackRowArr[i]);

                Data.Pieces.Where(o => o.Line == lineCount - 1 - i).ToList().ForEach(o =>
                {
                    //Grid.SetColumn(o, 0);
                    //Grid.SetRow(o, lineCount  - i -1);
                    stackRowArr[i].Children.Add(o);
                });

                i++;
            }
            #endregion
            BodyGrid.Children.Add(SelBorder);
            ViewBoxMain.Child = BodyGrid;
        }

        BorderSelection SelBorder = new BorderSelection() { Visibility = Visibility.Collapsed };
        int sel = 0;
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Data != null)
            {
                Selection(sel, 3);
                if (sel < Data.Pieces.Count() - 1)
                {
                    sel++;
                }
                else
                {
                    sel = 0;
                }
            }
        }

        public void Selection(int id, int length, bool end = true)
        {
            var piece = Data.Pieces.Where(o => o.Id == id).FirstOrDefault();
            if (piece != null)
            {
                var leftMargin = Data.Pieces.Where(o => o.Id < piece.Id && o.Line == piece.Line).Sum(o => o.Width);
                
                var width = 0;
                if (!end)
                    width = (int)Data.Pieces.Where(o => o.Id >= piece.Id && o.Line == piece.Line).Take(length).Sum(o => o.Width);
                else
                    width = (int)(Data.Width - leftMargin);

                SelBorder.Width = width;
                SelBorder.Height = piece.Height;
                SelBorder.Visibility = Visibility.Visible;
                SelBorder.HorizontalAlignment = HorizontalAlignment.Left;
                SelBorder.VerticalAlignment = VerticalAlignment.Bottom;
                SelBorder.Margin = new Thickness(leftMargin, 0, 0, 0);

                Grid.SetColumn(SelBorder, 0);
                Grid.SetRow(SelBorder, lineCount - piece.Line);
            }
        }
    }

    public class BoardBase
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public List<BoardPiece> Pieces { get; set; }

        public BoardBase(double width, double height)
        {
            Width = width; Height = height;
            Pieces = new List<BoardPiece>();
            Pieces.Add(new BoardPiece(100, 200) { Id = 0, Line = 0, });
            Pieces.Add(new BoardPiece(150, 180) { Id = 1, Line = 0, });
            Pieces.Add(new BoardPiece(200, 180) { Id = 2, Line = 0, });
            Pieces.Add(new BoardPiece(300, 150) { Id = 3, Line = 0, });
            Pieces.Add(new BoardPiece(300, 300) { Id = 4, Line = 1, });
            Pieces.Add(new BoardPiece(100, 280) { Id = 5, Line = 1, });
            Pieces.Add(new BoardPiece(200, 280) { Id = 6, Line = 1, });
            Pieces.Add(new BoardPiece(100, 150) { Id = 7, Line = 1, });
            Pieces.Add(new BoardPiece(298, 30) { Id = 8, Line = 1, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 1, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 1, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 2, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 2, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 2, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 2, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 2, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 3, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 3, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 3, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 3, });
            //Pieces.Add(new BoardPiece(Width = 100, Height = 20 ) { Id = 0, Line = 3, });
        }
    }

    public class BoardPiece : BorderCustom
    {
        public int Id { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        public BoardPiece(double width, double height)
        {
            base.Width = width;
            base.Height = height;
            //base.Background = new SolidColorBrush(Colors.Brown);
            //base.BorderBrush = new SolidColorBrush(Colors.Black);
            //base.BorderThickness = new Thickness(0.6);
            base.HorizontalAlignment = HorizontalAlignment.Left;
            base.VerticalAlignment = VerticalAlignment.Bottom;
        }
    }
}
