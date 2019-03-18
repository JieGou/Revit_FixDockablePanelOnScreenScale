namespace Revit_FixDockablePanelOnScreenScale
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ScreenScaleDecorator : Decorator
    {
        public ScreenScaleDecorator()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var presentationSource = PresentationSource.FromVisual(this);
            if (presentationSource != null)
            {
                if (presentationSource.CompositionTarget != null)
                {
                    Matrix m = presentationSource.CompositionTarget.TransformFromDevice;
                    ScaleTransform dpiTransform = new ScaleTransform(m.M11, m.M22);
                    if (dpiTransform.CanFreeze)
                        dpiTransform.Freeze();
                    RenderTransform = dpiTransform;

                    // Масштабируем текст обратно
                    if (Parent is Page page)
                        page.FontSize *= 1 / m.M11;
                }

                // Обязательно нужно отписаться, иначе декоратор будет срабатывать повторно, 
                // когда будут закрыты все документы и открыт новый. Содержимое панели
                // будет масштабироваться в геометрической прогрессии
                Loaded -= OnLoaded;
            }

        }
    }
}
