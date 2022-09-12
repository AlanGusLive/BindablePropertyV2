namespace BindableProperty;

using System.Diagnostics;
using Microsoft.Maui.Controls;

public partial class ImageMaui : ContentView
{
    public static readonly BindableProperty SourceProperty =
        BindableProperty.Create(
            "Source",
            typeof(string),
            typeof(ImageMaui),
            String.Empty,
            BindingMode.TwoWay,
            null,
            OnSourceChanged);

    public static readonly BindableProperty RotationMauiProperty =
        BindableProperty.Create(
            "RotationMaui",
            typeof(double),
            typeof(ImageMaui),
            0.0,
            BindingMode.TwoWay,
            null,
            OnRotationChanged);

    public ImageMaui()
    {
        InitializeComponent();
    }
    private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        string strSource = newValue as string;

        if (bindable is ImageMaui Im && !String.IsNullOrWhiteSpace(strSource))
        {
            Application.Current.Dispatcher.Dispatch(delegate
            {
                // The goal is to avoid to load each time the image from the file but to load it from a cache in memory.
                Im.ImageUserControl.Source = ImageSource.FromFile(Im.Source);
            });
        }
    }

    public string Source
    {
        get { return (string)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    private static void OnRotationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ImageMaui Im)
        {
            Application.Current.Dispatcher.Dispatch(delegate
            {
                Im.ImageUserControl.Rotation = (double)newValue;
            });
        }
        else
        {
            Debug.Assert(false);
        }
    }

    public double RotationMaui
    {
        get { return (double)GetValue(RotationMauiProperty); }
        set { SetValue(RotationMauiProperty, value); }
    }
}