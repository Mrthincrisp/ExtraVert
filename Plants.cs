using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

public record Plants
    (
    string Species,
    int LightNeeds,
    decimal AskingPrice,
    string City,
    string ZIP,
    bool Sold,
    DateTime AvailableUntil
    );
