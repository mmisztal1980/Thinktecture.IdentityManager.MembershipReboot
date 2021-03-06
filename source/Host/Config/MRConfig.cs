﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using BrockAllen.MembershipReboot;
using Thinktecture.IdentityManager.Host.Config;

namespace Thinktecture.IdentityManager.Host
{
    public class MRConfig
    {
        public static readonly MembershipRebootConfiguration<CustomUser> config;

        static MRConfig()
        {
            config = new MembershipRebootConfiguration<CustomUser>();
            config.PasswordHashingIterationCount = 10000;
            config.RequireAccountVerification = false;
            //config.EmailIsUsername = true;
        }
    }
}