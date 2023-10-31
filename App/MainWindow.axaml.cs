using App.Helpers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lib;
using Lib.Drawing;
using Lib.Drawing.Canvas;
using Lib.Drawing.Text;
using Lib.Helpers;
using Point = Lib.Drawing.Canvas.Point;

namespace App;

public partial class MainWindow : Window
{
    private IDrawableMatrix? _matrix;
    private readonly Canvas _canvas;
    private readonly TextBox _textBox;

    public MainWindow()
    {
        InitializeComponent();

        _matrix = null;
        _canvas = this.FindControl<Canvas>("Canvas")!;
        _textBox = this.FindControl<TextBox>("TextBox")!;
    }

    private void MatrixGeneratorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _matrix = new Matrix(5, 5);
        MatrixInitiator.FillMatrix(_matrix, 20, 20);

        UpdateCanvas();
        UpdateText();
    }

    private void SparseMatrixGeneratorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _matrix = new SparseMatrix(5, 5);
        MatrixInitiator.FillMatrix(_matrix, 4, 20);

        UpdateCanvas();
        UpdateText();
    }

    private void UpdateCanvas()
    {
        _canvas.Children.Clear();

        IMatrixDrawer drawer = new MatrixCanvasDrawer(
            new OriginOffsetDecorator(
                new AvaloniaCanvasAdapter(_canvas),
                new Point(15, 15)));
        _matrix?.Draw(drawer);
    }

    private void UpdateText()
    {
        IMatrixDrawer drawer = new MatrixTextDrawer(new AvaloniaTextBoxAdapter(_textBox));
        _matrix?.Draw(drawer);
    }
}
