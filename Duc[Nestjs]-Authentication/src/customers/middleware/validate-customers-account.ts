import { NestMiddleware } from "@nestjs/common";
import { NextFunction, Request, Response } from "express";

export class ValidateCustomerAccount implements NestMiddleware {
    use(req: Request, res: Response, next: NextFunction) {
        console.log('HEllo dkm');
        const { valid } = req.headers;
        if (valid) {
            next()
        } else {
            res.status(401).send({ error: 'Account is valid' })
        }



    }
}