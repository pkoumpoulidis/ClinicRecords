using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;

namespace ClinicRecords.Controls;

public enum IconPlacement
{
    Left,
    Right,
    Top,
    Bottom
}

public class IconButton : TemplatedControl
{
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<IconButton, string?>(nameof(Text));
    
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<Geometry?> IconProperty =
        AvaloniaProperty.Register<IconButton, Geometry?>(nameof(Icon));

    public Geometry? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    
    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<IconButton, double>(nameof(IconSize));

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public static readonly StyledProperty<IBrush> IconColorProperty =
        AvaloniaProperty.Register<IconButton, IBrush>(nameof(IconColor), Brushes.Blue);

    public IBrush IconColor
    {
        get => GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }
    
    public static readonly StyledProperty<IconPlacement> IconPlacementProperty =
        AvaloniaProperty.Register<IconButton, IconPlacement>(nameof(IconPlacement), IconPlacement.Left);

    public IconPlacement IconPlacement
    {
        get => GetValue(IconPlacementProperty);
        set => SetValue(IconPlacementProperty, value);
    }
    
    public static readonly StyledProperty<double> SpacingProperty =
        AvaloniaProperty.Register<IconButton, double>(nameof(Spacing), 6);

    public double Spacing
    {
        get => GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }
    
    private Thickness _textMargin;
    
    public static readonly DirectProperty<IconButton, Thickness> TextMarginProperty =
        AvaloniaProperty.RegisterDirect<IconButton, Thickness>(
            nameof(TextMargin),
            o => o.TextMargin);

    public Thickness TextMargin
    {
        get => _textMargin;
        private set => SetAndRaise(TextMarginProperty, ref _textMargin, value);
    }
    
    private void UpdateTextMargin()
    {
        TextMargin = IconPlacement switch
        {
            IconPlacement.Left => new Thickness(Spacing, 0, 0, 0),
            IconPlacement.Right => new Thickness(0, 0, Spacing, 0),
            IconPlacement.Top => new Thickness(0, Spacing, 0, 0),
            IconPlacement.Bottom => new Thickness(0, 0, 0, Spacing),
            _ => new Thickness(0)
        };
    }

    public static readonly StyledProperty<IBrush?> HoverBackgroundProperty =
        AvaloniaProperty.Register<IconButton, IBrush?>(nameof(HoverBackground));

    public IBrush? HoverBackground
    {
        get => GetValue(HoverBackgroundProperty);
        set => SetValue(HoverBackgroundProperty, value);
    }
    
    public static readonly StyledProperty<IBrush?> HoverForegroundProperty =
        AvaloniaProperty.Register<IconButton, IBrush?>(nameof(HoverForeground));

    public IBrush? HoverForeground
    {
        get => GetValue(HoverForegroundProperty);
        set => SetValue(HoverForegroundProperty, value);
    }
    
    public static readonly StyledProperty<IBrush?> HoverIconColorProperty =
        AvaloniaProperty.Register<IconButton, IBrush?>(nameof(HoverIconColor));

    public IBrush? HoverIconColor
    {
        get => GetValue(HoverIconColorProperty);
        set => SetValue(HoverIconColorProperty, value);
    }
    
    public static readonly StyledProperty<IBrush?> PressedBackgroundProperty =
        AvaloniaProperty.Register<IconButton, IBrush?>(nameof(PressedBackground));

    public IBrush? PressedBackground
    {
        get => GetValue(PressedBackgroundProperty);
        set => SetValue(PressedBackgroundProperty, value);
    }
    
    public static readonly StyledProperty<IBrush?> PressedForegroundProperty =
        AvaloniaProperty.Register<IconButton, IBrush?>(nameof(PressedForeground));

    public IBrush? PressedForeground
    {
        get => GetValue(PressedForegroundProperty);
        set => SetValue(PressedForegroundProperty, value);
    }
    
    public static readonly StyledProperty<IBrush?> PressedIconColorProperty =
        AvaloniaProperty.Register<IconButton, IBrush?>(nameof(PressedIconColor));

    public IBrush? PressedIconColor
    {
        get => GetValue(PressedIconColorProperty);
        set => SetValue(PressedIconColorProperty, value);
    }
    
    public static readonly StyledProperty<TimeSpan?> TransitionDurationProperty =
        AvaloniaProperty.Register<IconButton, TimeSpan?>(nameof(TransitionDuration));

    public TimeSpan? TransitionDuration
    {
        get => GetValue(TransitionDurationProperty);
        set => SetValue(TransitionDurationProperty, value);
    }
    
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            PseudoClasses.Set(":pressed", true);
            e.Handled = true;
        }
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        PseudoClasses.Set(":pressed", false);
        e.Handled = true;
    }

    protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
    {
        base.OnPointerCaptureLost(e);

        PseudoClasses.Set(":pressed", false);
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);

        PseudoClasses.Set(":pressed", false);
    }
    
    
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IconPlacementProperty)
        {
            UpdateIconPlacementPseudoClasses();
            UpdateTextMargin();
        }

        if (change.Property == SpacingProperty)
        {
            UpdateTextMargin();
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        UpdateIconPlacementPseudoClasses();
        UpdateTextMargin();
    }

    private void UpdateIconPlacementPseudoClasses()
    {
        PseudoClasses.Set(":icon-left", IconPlacement == IconPlacement.Left);
        PseudoClasses.Set(":icon-right", IconPlacement == IconPlacement.Right);
        PseudoClasses.Set(":icon-top", IconPlacement == IconPlacement.Top);
        PseudoClasses.Set(":icon-bottom", IconPlacement == IconPlacement.Bottom);
    }
}
