﻿// 11(c) Andrei Misiukevich
using System;
using FFImageLoading.Forms;
using PanCardView.Factory;
using Xamarin.Forms;

namespace PanCardViewSample.CardsFactory
{
    public static class RuleHolder
    {
        public static CardViewFactoryRule Rule { get; } = new CardViewFactoryRule
        {
            Creator = () =>
            {
                var content = new AbsoluteLayout();
                var frame = new Frame
                {
                    Padding = 0,
                    HasShadow = false,
                    CornerRadius = 10,
                    IsClippedToBounds = true
                };
                frame.SetBinding(VisualElement.BackgroundColorProperty, "Color");
                content.Children.Add(frame, new Rectangle(.5, .5, 300, 300), AbsoluteLayoutFlags.PositionProportional);

                var image = new CachedImage
                {
                    Aspect = Aspect.AspectFill
                };

                frame.Content = image;


                content.BindingContextChanged += (sender, e) => {
                    if (content?.BindingContext == null)
                    {
                        return;
                    }

                    image.Source = ((dynamic)content.BindingContext).Source;
                };

                return content;
            }
        };
    }
}
