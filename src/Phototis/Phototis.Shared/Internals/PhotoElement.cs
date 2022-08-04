﻿using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Uno.Extensions;
using Uno.Foundation;
using Uno.UI.Runtime.WebAssembly;

namespace Phototis
{
    public sealed class PhotoElement : Border
    {
        #region Fields

        private HtmlImageElement htmlImageElement;

        private CompositeTransform compositeTransform;

        #endregion

        #region Ctor

        public PhotoElement()
        {
            this.RenderTransformOrigin = new Windows.Foundation.Point(0.5, 0.5);
            compositeTransform = new CompositeTransform() { ScaleX = 1, ScaleY = 1 };
            this.RenderTransform = compositeTransform;
        }

        #endregion

        #region Properties

        public string Id { get; set; }


        private double scaleX;

        public double ScaleX
        {
            get { return scaleX; }
            set
            {
                scaleX = value;
                compositeTransform.ScaleX = scaleX;
            }
        }

        private double scaleY;

        public double ScaleY
        {
            get { return scaleY; }
            set
            {
                scaleY = value;
                compositeTransform.ScaleY = scaleY;
            }
        }

        private double _Grayscale = 0;

        public double Grayscale
        {
            get { return _Grayscale; }
            set
            {
                _Grayscale = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Grayscale = _Grayscale;
            }
        }

        private double _Contrast = 100;

        public double Contrast
        {
            get { return _Contrast; }
            set
            {
                _Contrast = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Contrast = _Contrast;
            }
        }

        private double _Brightness = 100;

        public double Brightness
        {
            get { return _Brightness; }
            set
            {
                _Brightness = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Brightness = _Brightness;
            }
        }

        private double _Saturation = 100;

        public double Saturation
        {
            get { return _Saturation; }
            set
            {
                _Saturation = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Saturation = _Saturation;
            }
        }

        private double _Sepia = 0;

        public double Sepia
        {
            get { return _Sepia; }
            set
            {
                _Sepia = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Sepia = _Sepia;
            }
        }

        private double _Invert = 0;

        public double Invert
        {
            get { return _Invert; }
            set
            {
                _Invert = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Invert = _Invert;
            }
        }

        private double _Hue = 0;

        public double Hue
        {
            get { return _Hue; }
            set
            {
                _Hue = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Hue = _Hue;
            }
        }

        private double _Blur = 0;

        public double Blur
        {
            get { return _Blur; }
            set
            {
                _Blur = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Blur = _Blur;
            }
        }

        private double _Opacity = 1;

        public new double Opacity
        {
            get { return _Opacity; }
            set
            {
                _Opacity = value;

                if (htmlImageElement is not null)
                    htmlImageElement.Opacity = _Opacity;
            }
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(PhotoElement), new PropertyMetadata(default(string), OnSourceChanged));

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is PhotoElement image)
            {
                if (image.htmlImageElement is null)
                {
                    image.htmlImageElement = new HtmlImageElement() { Source = args.NewValue as string, Id = image.Id };
                    image.Child = image.htmlImageElement;
                }
                else
                {
                    image.htmlImageElement.Source = args.NewValue as string;
                    image.Child = image.htmlImageElement;
                }
            }
        }

        #endregion

        #region Methods

        public void Export()
        {
            var id = htmlImageElement.Id;
            var src = htmlImageElement.GetHtmlAttribute("src");
            var filter = htmlImageElement.GetCssFilter();
            var function = $"exportImage('{id}','{filter}','{src}')";

            WebAssemblyRuntime.InvokeJS(function);
        }

        public void Reset()
        {
            Grayscale = 0;
            Contrast = 100;
            Brightness = 100;
            Saturation = 100;
            Sepia = 0;
            Invert = 0;
            Hue = 0;
            Blur = 0;
            Opacity = 1;
            ScaleX = 1;
            ScaleY = 1;
        }

        #endregion
    }
}
