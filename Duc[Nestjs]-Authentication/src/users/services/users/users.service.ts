import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { plainToClass } from 'class-transformer';
import { User, User as UserEntity } from 'src/typeorm';
import { CreateUserDto } from 'src/users/dto/CreateUser.dto';
import { SerializedUser } from 'src/users/type';
import { Repository } from 'typeorm';


@Injectable()
export class UsersService {
    // private users: User[] = [
    //     {
    //         id: 1,
    //         username: 'Duc',
    //         password: 'Duc'
    //     },
    //     {
    //         id: 2,
    //         username: 'Minh',
    //         password: 'Minh'
    //     },
    //     {
    //         id: 3,
    //         username: 'Hoa',
    //         password: 'Hoa'
    //     },
    //     {
    //         id: 4,
    //         username: 'Mai',
    //         password: 'Mai'
    //     },
    // ];

    constructor(
        @InjectRepository(UserEntity)
        private readonly userRepository: Repository<UserEntity>,
    ) { }

    private users: User[] = []

    getAllUser() {
        return this.users.map((user) => new SerializedUser(user))
    }

    getUserByUserName(username: string) {
        return this.users.find((users) => users.usename === username)
    }
    getUserByUserId(id: number) {
        return this.users.find((users) => users.id === id)
    }

    createNewUser(createUserDto: CreateUserDto) {
        const newuser = this.userRepository.create(createUserDto)
        return this.userRepository.save(newuser)
    }
    findUserByUsername(usename: string) {
        return this.userRepository.findOne({
            where: {
                usename
            }
        });
    }
}
