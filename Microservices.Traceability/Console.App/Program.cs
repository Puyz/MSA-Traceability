﻿
using NLog;
using System.Diagnostics;

Logger _logger = LogManager.GetCurrentClassLogger();
Trace.CorrelationManager.ActivityId = Guid.NewGuid();

Work1();

void Work1()
{
    Console.WriteLine("Work1 tetiklendi");
    _logger.Debug("Work1 tetiklendi");
    Work2();
}
void Work2()
{
    Console.WriteLine("Work2 tetiklendi");
    _logger.Debug("Work2 tetiklendi");
    Work3();
}
void Work3()
{
    Console.WriteLine("Work3 tetiklendi");
    _logger.Debug("Work3 tetiklendi");
}