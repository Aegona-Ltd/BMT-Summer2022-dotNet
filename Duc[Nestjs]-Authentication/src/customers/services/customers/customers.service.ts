import { Injectable } from '@nestjs/common';
import { CreateCustomerDto } from 'src/customers/dtos/CreateCustomers.dto';

@Injectable()
export class CustomersService {
    customers = [
        {
            id: 1,
            email: 'hello@gmail.com',
            name: 'Duc'
        },
        {
            id: 2,
            email: 'hi@gmail.com',
            name: 'Minh'
        },
        {
            id: 3,
            email: 'le@gmail.com',
            name: 'Hoa'
        }
    ]
    findCustomerById(id: number) {
        return this.customers.find((user) => user.id === id)

    }
    createCustomer(customerDto: CreateCustomerDto) {
        this.customers.push(customerDto)
    }
    getAllCustomer() {
        return this.customers
    }
}
