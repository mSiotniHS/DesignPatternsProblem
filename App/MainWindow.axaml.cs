using System.Collections.Generic;
using App.Helpers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lib;
using Lib.Decorators;
using Lib.Drawing;
using Lib.Drawing.Canvas;
using Lib.Drawing.Text;
using Lib.Misc;
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
        var matrix1 = new Matrix(3, 3);
        var matrix2 = new SparseMatrix(4, 4);
        var matrix3 = new Matrix(2, 2);

        MatrixInitiator.FillMatrix(matrix1, 9, 10);
        MatrixInitiator.FillMatrix(matrix2, 3, 10);
        MatrixInitiator.FillMatrix(matrix3, 4, 10);

        _matrix = new MatrixHorizontalGroup(new List<IMatrix>
        {
            matrix1,
            matrix2,
            matrix3
        });

        // MatrixInitiator.FillMatrix(_matrix, 20, 20);

        UpdateCanvas();
        UpdateText();
    }

    private void SparseMatrixGeneratorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        // _matrix = new SparseMatrix(5, 5);
        // MatrixInitiator.FillMatrix(_matrix, 4, 20);

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

    private void RemoveDecoratorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _matrix = _matrix.GetOriginal();
        UpdateCanvas();
        UpdateText();
    }

    private void AddSwappingDecoratorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _matrix = new SwappedMatrix(_matrix, 0, 2);
        UpdateCanvas();
        UpdateText();
    }

    private void AddTransposeDecoratorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _matrix = new TransposedMatrix(_matrix);
        UpdateCanvas();
        UpdateText();
    }
}
