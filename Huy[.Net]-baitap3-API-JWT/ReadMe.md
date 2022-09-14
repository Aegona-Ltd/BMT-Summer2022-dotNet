STEP 1: git clone https://github.com/Aegona-Ltd/Summer2022-dotNet/.

STEP 2: Set up Xampp to use Sql.

STEP 3: Run Project

STEP 4: Migration and update Database.

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
