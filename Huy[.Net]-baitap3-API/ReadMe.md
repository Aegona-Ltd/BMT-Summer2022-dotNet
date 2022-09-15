STEP 1: git clone https://github.com/Aegona-Ltd/Summer2022-dotNet/.

STEP 2: Set up Xampp to use Sql.

STEP 3: Run Project

STEP 4: Migration and update Database.

![image](https://user-images.githubusercontent.com/94180311/188412787-cce9630f-0a1d-4eb5-9a27-91d79154b91b.png)
![image](https://user-images.githubusercontent.com/94180311/188412882-73335a9f-be0f-451b-8a0c-498bb704098d.png)
![image](https://user-images.githubusercontent.com/94180311/188413095-499f90d2-a1eb-4e3e-ab12-01a2ade7611f.png)
![image](https://user-images.githubusercontent.com/94180311/188413158-f2302527-888a-46d1-bd5d-9d8d2c158dd9.png)

Let Start with Account: Email:huy@gmail.com Password: Huy123456@ 

As you see when you run client, it does't have token and refreshToken in cookie
![image](https://user-images.githubusercontent.com/94180311/190116149-239fa28d-58d4-4bcd-b8b1-a57fe1579834.png)
After Login you will see token and refreshToken have been saved in cookie
![image](https://user-images.githubusercontent.com/94180311/190116270-3dd07f0e-f5b8-4482-a235-d30c33ac9d54.png)
Now You will see token will refresh almost limitless with refreshToken if it isn't expire

It only returns the login page when the RefreshToken has expired. So when does the RefreshToken expire,
I have set it to almost infinite if you stay logged in and it will expire if you are inactive between sites or you can be proactive with Logout
![image](https://user-images.githubusercontent.com/94180311/190116615-8db7d88c-78ea-492e-b8ba-18afad5a6c98.png)
You see that, after logout token and RefreshToken have been deleted in cookie.

Now if you try to access into contactList, it will only make you return Login Page
 
 I have created a button to down Excel File of Contact List

![image](https://user-images.githubusercontent.com/94180311/190353947-b931fc27-8ed4-4c57-8b84-33aeebe83f11.png)
![image](https://user-images.githubusercontent.com/94180311/190354263-4205bf91-918d-485e-8314-c9987e2b3968.png)



