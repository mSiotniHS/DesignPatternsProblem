using App.Helpers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lib;
using Lib.Drawing;
using Lib.Drawing.Canvas;
using Lib.Drawing.Text;
using Lib.Helpers;
using Lib.Modificators;
using Point = Lib.Drawing.Common.Point;

namespace App;

public partial class MainWindow : Window
{
    private IMatrix _matrix;
    private readonly Canvas _canvas;
    private readonly TextBox _textBox;
    private bool _showBorder;
    private bool _toDecorate;

    public MainWindow()
    {
        InitializeComponent();

        _matrix = new Matrix(5, 5);
        MatrixInitiator.FillMatrix(_matrix, 20, 20);

        _canvas = this.FindControl<Canvas>("Canvas")!;
        _textBox = this.FindControl<TextBox>("TextBox")!;

        _showBorder = true;
        _toDecorate = false;

        UpdateCanvas();
        UpdateText();
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
                new AvaloniaCanvas(_canvas),
                new Point(15, 15)));

        var visitor = new DrawingVisitor(_showBorder ? drawer : new BorderlessDrawer(drawer), _matrix);
        _matrix.AcceptVisitor(visitor);
    }

    private void UpdateText()
    {
        IMatrixDrawer drawer = new MatrixTextDrawer(new AvaloniaTextBoxTextarea(_textBox));

        var visitor = new DrawingVisitor(_showBorder ? drawer : new BorderlessDrawer(drawer), _matrix);
        _matrix.AcceptVisitor(visitor);
    }

    private void ShowBorderCheckbox_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        _showBorder = !_showBorder;
        UpdateCanvas();
        UpdateText();
    }

    private void AddDecoratorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _matrix = new SwappedMatrix(_matrix, 2, 2);
        UpdateCanvas();
        UpdateText();
    }

    private void RemoveDecoratorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _matrix = _matrix.GetOriginal();
        UpdateCanvas();
        UpdateText();
    }
}
