﻿using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services
{
    public enum ContentViewType
    {
        UserContent,
        GuestContent
    }
    public interface IContentViewFactory
    {
        ContentViewModel CreateContentViewModel(ContentViewType type);
    }
}