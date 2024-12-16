﻿namespace MauiGame.Services;

public interface IBackgroundService
{
    bool IsActive { get; }

    void Start();
    void Stop();
    //void SetTitle(string title);
}
