﻿namespace Epam.NetMentoring.Decorator.Calculator
{
    public interface IOperation
    {
        //IT: Result from divide/multiply is not always int
        int GetResult();
    }
}
