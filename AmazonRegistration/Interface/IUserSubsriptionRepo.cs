﻿using AmazonRegistration.Model;
using AmazonSellerApi.Model;

namespace AmazonSellerApi.Interface
{
    public interface IUserSubsriptionRepo
    {
        public Response SaveSubs(UserSubscription subscription);

    }
}
