using Blog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Business.Tools.JWTTool
{
    public interface IJwtService
    {

        JwtToken GenerateJwt(AppUser appUser);

    }
}
