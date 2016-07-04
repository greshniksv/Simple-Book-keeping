﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetPlansQuery : IRequest<IList<PlanModel>>
    {
        /// <summary>
        /// If value is TRUE, return only deleted plan; If FALSE, return only not deleted; If NULL return all;
        /// </summary>
        [DefaultValue(null)]
        public bool? ShowDeleted { get; set; }

        /// <summary>
        /// If value is TRUE, return only active plan; If FALSE, return only not active; If NULL return all;
        /// </summary>
        [DefaultValue(null)]
        public bool? IsActive { get; set; }

        public Guid UserId { get; set; }
    }
}