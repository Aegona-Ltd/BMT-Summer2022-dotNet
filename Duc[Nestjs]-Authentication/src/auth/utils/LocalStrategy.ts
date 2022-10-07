import { Inject, Injectable, UnauthorizedException } from "@nestjs/common";
import { PassportStrategy } from "@nestjs/passport";
import { Strategy } from "passport-local";
import { AuthService } from "../services/auth/auth.service";

@Injectable()
export class LocalStrategy extends PassportStrategy(Strategy) {
    constructor(
        @Inject('AUTH_SERVICE') private readonly authService: AuthService
    ) {
        super(
            { usernameField: 'email', }
        )
    }
    async validate(usename: string, password: string) {
        console.log('Inside LocalStrategy.validate');
        console.log(usename);
        console.log(password);


        const user = await this.authService.validateUser(usename, password)
        if (!user) {
            throw new UnauthorizedException();
        }
        return user
    }
}