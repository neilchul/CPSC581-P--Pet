using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Project1_Pet
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Ellipse touchPoint;
        SolidColorBrush Red = new SolidColorBrush(Colors.Red);

        public MainPage()
        {


            this.InitializeComponent();

            this.Container.PointerPressed += OnPointerPressed;
            this.Container.PointerMoved += OnPointerMoved;
            this.Container.PointerReleased += OnPointerReleased;

            this.touchPoint = new Ellipse()
            {
                Width = 100,
                Height = 100,
                Fill = Red,
                RenderTransform = new CompositeTransform()
            };
        }

        double toDegrees(double radians)
        {
            return (180/Math.PI) * radians;
        }

        void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
           
            //switch face
            this.IdleShiba.Visibility =  Windows.UI.Xaml.Visibility.Collapsed;
            this.HappyShiba.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //red dot
            (this.touchPoint.RenderTransform as CompositeTransform).TranslateX = e.GetCurrentPoint(this.Container).Position.X - this.touchPoint.Width / 2;
            (this.touchPoint.RenderTransform as CompositeTransform).TranslateY = e.GetCurrentPoint(this.Container).Position.Y - this.touchPoint.Height / 2;

            this.Container.Children.Add(this.touchPoint);

            if (!this.Container.Children.Contains(this.touchPoint))
            {
                this.Container.Children.Add(this.touchPoint);
            }

        }

        void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {

            //switch face back
            this.IdleShiba.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.HappyShiba.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            this.Container.Children.Remove(this.touchPoint);
        }

        void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var pointX = e.GetCurrentPoint(this.Container).Position.X;
            var pointY = e.GetCurrentPoint(this.Container).Position.Y;

            (this.touchPoint.RenderTransform as CompositeTransform).TranslateX = pointX - this.touchPoint.Width / 2;
            (this.touchPoint.RenderTransform as CompositeTransform).TranslateY = pointY - this.touchPoint.Height / 2;

            //rotating pupper face
            var newX = (pointX - (66 + (this.HappyShiba.Width / 2)));
            var newY = (pointY - (384 + (this.HappyShiba.Height / 2)));
            var powX = Math.Pow(newX, 2);
            var powY = Math.Pow(newY, 2);
            var distance = Math.Sqrt(powX + powY);
            var result = newX / distance;

            var angle = toDegrees(Math.Asin(result));

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = angle;
            this.HappyShiba.RenderTransform = rotateTransform;
        }
    }
}
