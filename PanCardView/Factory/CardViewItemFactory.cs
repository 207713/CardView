﻿
using System;
using Xamarin.Forms;

namespace PanCardView.Factory
{
    public class CardViewItemFactory
    {
        private readonly CardViewFactoryRule _defaultRule;

        public CardViewItemFactory() : this(default(CardViewFactoryRule))
        {
        }

        public CardViewItemFactory(Func<View> creator) : this(new CardViewFactoryRule(creator))
        {
        }

        public CardViewItemFactory(CardViewFactoryRule defaultRule)
        => _defaultRule = defaultRule;

        public virtual CardViewFactoryRule GetRule(object context) 
        => _defaultRule;
    }
}
