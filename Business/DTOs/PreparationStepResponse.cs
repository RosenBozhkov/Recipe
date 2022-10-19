using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs;

public record PreparationStepResponse(
    int Number,
    string Description,
    TimeSpan Duration);
