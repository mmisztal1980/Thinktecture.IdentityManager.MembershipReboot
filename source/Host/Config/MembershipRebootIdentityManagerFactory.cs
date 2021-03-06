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
using BrockAllen.MembershipReboot.Ef;
using Thinktecture.IdentityManager.MembershipReboot;

namespace Thinktecture.IdentityManager.Host.Config
{
    public class MembershipRebootIdentityManagerFactory
    {
        private string connString;

        public MembershipRebootIdentityManagerFactory(string connString)
        {
            this.connString = connString;
        }

        public IIdentityManagerService Create()
        {
            var db = new CustomDatabase(connString);
            var userRepo = new DbContextUserAccountRepository<CustomDatabase, CustomUser>(db);
            userRepo.QueryFilter = RelationalUserAccountQuery<CustomUser>.Filter;
            userRepo.QuerySort = RelationalUserAccountQuery<CustomUser>.Sort;
            var userSvc = new UserAccountService<CustomUser>(MRConfig.config, userRepo);

            var groupRepo = new DbContextGroupRepository<CustomDatabase, CustomGroup>(db);
            var groupSvc = new GroupService<CustomGroup>(MRConfig.config.DefaultTenant, groupRepo);

            MembershipRebootIdentityManagerService<CustomUser, CustomGroup> idMgr = null;
            idMgr = new MembershipRebootIdentityManagerService<CustomUser, CustomGroup>(userSvc, userRepo, groupSvc, groupRepo);

            // uncomment to allow additional properties mapped to claims
            //idMgr = new MembershipRebootIdentityManagerService<CustomUser, CustomGroup>(userSvc, userRepo, groupSvc, groupRepo, () =>
            //{
            //    var meta = idMgr.GetStandardMetadata();
            //    meta.UserMetadata.UpdateProperties =
            //        meta.UserMetadata.UpdateProperties.Union(
            //            new PropertyMetadata[] {
            //                idMgr.GetMetadataForClaim(Constants.ClaimTypes.Name, "Name")
            //            }
            //        );
            //    return Task.FromResult(meta);
            //});

            return new DisposableIdentityManagerService(idMgr, db);
        }
    }
}