﻿namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IPresenter<T>
    {
        T View { get;  }
        void Initialize();
    }
}