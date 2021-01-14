# Abp.VerificationCode

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FAbp.VerificationCode%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.VerificationCode.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.VerificationCode)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.VerificationCode.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.VerificationCode)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/Abp.VerificationCode?style=social)](https://www.github.com/EasyAbp/Abp.VerificationCode)

An ABP module to generate and verify verification codes.

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.Abp.VerificationCode
    * (Optional) EasyAbp.Abp.VerificationCode.Identity

1. Add `DependsOn(typeof(AbpVerificationCodeXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

## Usage

### Generate and Validate

1. Generate a verification code.
    ```csharp
    var verificationCodeManager = ServiceProvider.GetRequiredService<IVerificationCodeManager>();
    
    var code = await verificationCodeManager.GenerateAsync(
        codeCacheKey: $"DangerousOperationPhoneVerification:{phoneNumber}",
        codeCacheLifespan: TimeSpan.FromMinutes(3),
        configuration: new VerificationCodeConfiguration());
    
    await _smsSender.SendAsync(new SmsMessage(phoneNumber, $"Your code is: {code}"));
    // you can also use the EasyAbp.NotificationService module and send the code to users.
    ```

2. Validate a verification code.
    ```csharp
    var verificationCodeManager = ServiceProvider.GetRequiredService<IVerificationCodeManager>();
    
    var result = await verificationCodeManager.ValidateAsync(
        codeCacheKey: $"DangerousOperationPhoneVerification:{phoneNumber}",
        verificationCode: code,
        configuration: new VerificationCodeConfiguration());
    ```

### Use as UserManager Token Providers

Please install the `EasyAbp.Abp.VerificationCode.Identity` module, it registers the token providers that uses `IVerificationCodeManager`.

## Q&A

### How to Change the Code Generation Strategy

You can instantiate a custom [VerificationCodeConfiguration](https://github.com/EasyAbp/Abp.VerificationCode/blob/master/src/EasyAbp.Abp.VerificationCode/EasyAbp/Abp/VerificationCode/VerificationCodeConfiguration.cs), for example:

```csharp
var configuration = new VerificationCodeConfiguration(
    length: 6,
    chars: "0123456789abcdefghijklmnopqrstuvwxyz",
    equivalentCharsMaps: new Dictionary<char, IEnumerable<char>>
    {
        {'0', new[] {'o', 'O'}},
        {'1', new[] {'l', 'L'}},
    }); // "oL2345" will also be verified if the correct code is "012345"
```

## Road map

Todo.
