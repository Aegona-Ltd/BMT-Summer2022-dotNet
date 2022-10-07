import { IsEmail, IsNotEmpty, MinLength } from "class-validator";


export class CreateUserDto {
    @IsNotEmpty()
    @MinLength(3)
    usename: string;
    @IsNotEmpty()
    @MinLength(6)
    password: string;
    @IsNotEmpty()
    @IsEmail()
    emailAddress: string
}