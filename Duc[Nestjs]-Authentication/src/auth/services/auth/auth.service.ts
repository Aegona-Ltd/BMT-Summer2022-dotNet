import { Inject, Injectable } from '@nestjs/common';
import { UsersService } from 'src/users/services/users/users.service';

@Injectable()
export class AuthService {
    constructor(
        @Inject('USER_SERVICE') private readonly userService: UsersService
    ) { }
    async validateUser(usename: string, password: string) {
        console.log('Inside validateUser');

        const userDB = await this.userService.findUserByUsername(usename)
        if (userDB && userDB.password === password) {
            console.log('Thành công');
            return userDB

        }
        return null
    }
}
