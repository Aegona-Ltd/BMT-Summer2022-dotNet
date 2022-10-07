import { IsNotEmpty } from "class-validator";

export class CreateAddressDto {
    @IsNotEmpty()
    city: string;
    @IsNotEmpty()
    rip: string;
}