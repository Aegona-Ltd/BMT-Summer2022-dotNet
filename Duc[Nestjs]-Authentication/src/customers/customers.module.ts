import { MiddlewareConsumer, Module, NestModule, RequestMethod } from '@nestjs/common';
import { NextFunction, request } from 'express';
import { CustomersController } from './controllers/customers/customers.controller';
import { ValidateCustomerAccount } from './middleware/validate-customers-account';
import { ValidateCustomerMiddleware } from './middleware/validate-cutomers';
import { CustomersService } from './services/customers/customers.service';

@Module({
  controllers: [CustomersController],
  providers: [CustomersService]
})
export class CustomersModule implements NestModule {
  configure(consumer: MiddlewareConsumer) {
    //  consumer.apply(ValidateCustomerMiddleware).forRoutes(
    //    {
    //       path: 'customers/search/:id',
    //       method: RequestMethod.GET
    //    },
    //    {
    //      path: 'customers/:id',
    //      method: RequestMethod.GET
    //    }
    //   )
    consumer.apply(ValidateCustomerMiddleware, ValidateCustomerAccount,
      (req: Request, res: Response, next: NextFunction) => {
        console.log('Last Middleware');
        next();

      }
    )
      .exclude({
        path: 'api/customers/create',
        method: RequestMethod.POST
      })
      .forRoutes(CustomersController)
  }
}
