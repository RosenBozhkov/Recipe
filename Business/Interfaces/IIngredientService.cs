﻿using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface IIngredientService
{
    Task<Ingredient> GetOrCreateAsync(string name);
}
