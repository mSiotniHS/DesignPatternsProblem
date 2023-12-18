using System;
using App.CommandFramework;
using App.Helpers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lib;
using Lib.Drawing;
using Lib.Drawing.Canvas;
using Lib.Drawing.Text;
using Lib.Misc;
using Point = Lib.Drawing.Common.Point;

namespace App;

public partial class MainWindow : Window
{
    private IMatrix? _matrix;
    private readonly IMatrix _initial;
    private readonly Canvas _canvas;
    private readonly TextBox _textBox;

    public MainWindow()
    {
        InitializeComponent();

        _canvas = this.FindControl<Canvas>("Canvas")!;
        _textBox = this.FindControl<TextBox>("TextBox")!;

        _initial = new Matrix(5, 6);
        MatrixInitiator.FillMatrix(_initial, 25, 20);

        new InitializeAppStateCommand(this, _initial).Execute();

        Update();
    }

    private void Update()
    {
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

        var visitor = new DrawingVisitor(drawer, _matrix);
        _matrix.AcceptVisitor(visitor);
    }

    private void UpdateText()
    {
        IMatrixDrawer drawer = new MatrixTextDrawer(new AvaloniaTextBoxTextarea(_textBox));

        var visitor = new DrawingVisitor(drawer, _initial);
        _initial.AcceptVisitor(visitor);
    }

    private void ChangeMatrixButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var row = (uint) Random.Shared.Next(0, (int) _matrix.RowCount);
        var column = (uint) Random.Shared.Next(0, (int) _matrix.ColumnCount);
        var value = 20 * (Random.Shared.NextDouble() - 0.5);

        new ChangeMatrixCommand(_matrix, row, column, value).Execute();

        Update();
    }

    private void UndoButton_OnClick(object? sender, RoutedEventArgs e)
    {
        CommandManager.Instance.Undo();
        Update();
    }

    #region Commands

    private class InitializeAppStateCommand : ACommand
    {
        private readonly MainWindow _window;
        private readonly IMatrix _matrix;

        public InitializeAppStateCommand(MainWindow window, IMatrix matrix)
        {
            _window = window;
            _matrix = matrix;
        }

        protected override void DoExecute()
        {
            if (_window._matrix is null)
            {
                _window._matrix = _matrix.Clone();
                return;
            }

            for (var i = 0u; i < _window._matrix.RowCount; i++)
            {
                for (var j = 0u; j < _window._matrix.ColumnCount; j++)
                {
                    _window._matrix.Set(i, j, _matrix.Get(i, j));
                }
            }
        }
    }

    private class ChangeMatrixCommand : ACommand
    {
        private readonly IMatrix _matrix;
        private readonly uint _row;
        private readonly uint _column;
        private readonly double _value;

        public ChangeMatrixCommand(IMatrix matrix, uint row, uint column, double value)
        {
            _matrix = matrix;
            _row = row;
            _column = column;
            _value = value;
        }

        protected override void DoExecute()
        {
            _matrix.Set(_row, _column, _value);
        }
    }

    #endregion
}
