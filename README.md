# GoGreen

IISEXPRESS ERROR when launching project
modify the applicationhost.config on GoGreenV3/.vs/config/ then change the following code:

<binding protocol="https" bindingInformation="*:44300:localhost" />
change to
<binding protocol="https" bindingInformation="*:44300:*" />

then delete the code below it
<binding protocol="http" bindingInformation="*:5425:localhost" />

## License

See the [LICENSE](LICENSE.md) file for license rights and limitations (MIT).
