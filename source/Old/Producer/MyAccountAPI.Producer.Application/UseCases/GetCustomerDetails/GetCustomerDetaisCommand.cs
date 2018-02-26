﻿namespace MyAccountAPI.Producer.Application.UseCases.GetCustomerDetails
{
    using System;

    public class GetCustomerDetaisCommand
    {
        public Guid CustomerId { get; private set; }
        public GetCustomerDetaisCommand(Guid customerId)
        {
            this.CustomerId = customerId;
        }
    }
}
