﻿using System;
using System.Threading.Tasks;
using PanCardView.Enums;
using Xamarin.Forms;

namespace PanCardView.Processors
{
    public class BaseCarouselFrontViewProcessor : ICardProcessor
    {
        public uint AnimationLength { get; set; } = 300;

        public Easing AnimEasing { get; set; } = Easing.SinInOut;

        public virtual void InitView(View view, CardsView cardsView, PanItemPosition panItemPosition)
        {
            if (view != null)
            {
                view.TranslationX = 0;
                view.IsVisible = true;
            }
        }

        public virtual void AutoNavigate(View view, CardsView cardsView, PanItemPosition panItemPosition)
        {
            if (view != null)
            {
                view.IsVisible = true;
                new Animation(v => view.TranslationX = v, view.TranslationX, 0)
                    .Commit(view, nameof(AutoNavigate), 16, AnimationLength, AnimEasing);
            }
        }

        public virtual void HandlePanChanged(View view, CardsView cardsView, double xPos, PanItemPosition panItemPosition)
        {
            if (Math.Abs(xPos) > cardsView.Width || (panItemPosition == PanItemPosition.Prev && xPos < 0) || (panItemPosition == PanItemPosition.Next && xPos > 0))
            {
                return;
            }
            view.TranslationX = xPos;
        }

        public virtual Task HandlePanReset(View view, CardsView cardsView, PanItemPosition panItemPosition)
        {
            if (view != null)
            {
                var tcs = new TaskCompletionSource<bool>();
                var animTimePercent = 1 - (cardsView.Width - Math.Abs(view.TranslationX)) / cardsView.Width;
                var animLength = (uint)(AnimationLength * animTimePercent);
                new Animation(v => view.TranslationX = v, view.TranslationX, 0)
                    .Commit(view, nameof(HandlePanApply), 16, animLength, AnimEasing, (v, t) => tcs.SetResult(true));
                return tcs.Task;
            }
            return Task.FromResult(true);
        }

        public virtual Task HandlePanApply(View view, CardsView cardsView, PanItemPosition panItemPosition)
        {
            if (view != null)
            {
                var tcs = new TaskCompletionSource<bool>();
                var animTimePercent = 1 - (cardsView.Width - Math.Abs(view.TranslationX)) / cardsView.Width;
                var animLength = (uint)(AnimationLength * animTimePercent);
                new Animation(v => view.TranslationX = v, view.TranslationX, 0)
                    .Commit(view, nameof(HandlePanApply), 16, animLength, AnimEasing, (v, t) => tcs.SetResult(true));
                return tcs.Task;
            }
            return Task.FromResult(true);
        }
    }
}
