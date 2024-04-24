/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:02
** desc: 用户信息领域模型
** Ver : V1.0.0
**********************************************************************/

using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using ZsjTest.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace ZsjTest.Domain.UserMgr;

public class UserInfo
{
    public int UserId { get; set; }

    public int? UserType { get; set; }

    public string UserGuid { get; set; } = null!;

    public string? UserNumber { get; set; }

    public string? EngLastName { get; set; }

    public string? EngMidName { get; set; }

    public string? EngFirstName { get; set; }

    public string? NativeName { get; set; }

    public bool? Readonly { get; set; }

    public string? LoginName { get; set; }

    public string? LowercaseLoginName { get; set; }

    public byte[]? Password { get; set; }

    public int? CityId { get; set; }

    public string? DirectLine { get; set; }

    public string? PhoneExt { get; set; }

    public string? OfficeMobile { get; set; }

    public string? FaxNum { get; set; }

    public string? Email { get; set; }

    public int? EmployeeId { get; set; }

    public bool? EnableFlag { get; set; }

    public bool? DeleteFlag { get; set; }

    public string? PasswordQuestion { get; set; }

    public byte[]? PasswordAnswer { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? LastPasswordchangedDate { get; set; }

    public bool? OnlineFlag { get; set; }

    public bool? LockedoutFlag { get; set; }

    public DateTime? LastLockedoutDate { get; set; }

    public bool? ApproveFlag { get; set; }

    public int? Failedpasswordattemptcount { get; set; }

    public DateTime? Failedpasswordattemptwndwstart { get; set; }

    public int? Failedpasswordanswerattmptcnt { get; set; }

    public DateTime? Failedpasswordanswerattmpstart { get; set; }

    public string? Remark { get; set; }

    public bool? PasswordExpiredFlag { get; set; }

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int? LastUpdateBy { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public DateTime? AccountDisableDate { get; set; }

    public int? ConsId { get; set; }

    public bool? LeaveNoticeMailedFlag { get; set; }

    public async Task<JwtToken> BuildNewTokenAsync(AppSettings appSettings)
    {
        string tokenJti = Guid.NewGuid().ToString("N");
        return await CreateTokenAsync(tokenJti, appSettings);
    }

    private async Task<JwtToken> CreateTokenAsync(string jti, AppSettings appSettings)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, UserId.ToString()),
            new Claim(ClaimTypes.Role, PubConst.ApplicationApiName),
            new Claim(JwtRegisteredClaimNames.Jti, jti),
            new Claim(PubConst.JwtUserNameKey, NativeName!)
        };

        RSA rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(appSettings.SigningCredential), out _);
        var secretKey = new RsaSecurityKey(rsa);
        var algorithm = SecurityAlgorithms.RsaSha256;
        var signingCredentials = new SigningCredentials(secretKey, algorithm);

        var jwtSecurityToken = new JwtSecurityToken(
            appSettings.Issuer,
            appSettings.Aud,
            claims,
            DateTime.Now,
            DateTime.Now.AddSeconds(appSettings.ClientTokenLifeTime),
            signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return await Task.FromResult(new JwtToken
        {
            Access_token = token,
            Expires_in = appSettings.ClientTokenLifeTime,
            Token_type = PubConst.DefaultTokenType,
        });
    }
}
