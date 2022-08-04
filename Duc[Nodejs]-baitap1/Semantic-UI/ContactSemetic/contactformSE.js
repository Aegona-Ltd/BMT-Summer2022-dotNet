
$('.ui.form')
    .form({
        fields: {
            email: {
                identifier: 'email',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your e-mail'
                    },
                    {
                        type: 'email ',
                        prompt: '{name} must be a valid e-mail'

                    }
                ]
            },

            password: {
                identifier: 'password',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your password'
                    },
                    {
                        type: 'minLength[6]',
                        prompt: 'Your password must be at least {ruleValue} characters'
                    },

                    {
                        type: 'regExp[/^[a-z0-9_-]{4,16}$/]',
                        prompt: 'Please enter a 4-16 letter username'
                    },
                ]
            },

            Firstname: {
                identifier: 'firstname',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your firstname'
                    },

                ]
            },

            Lastname: {
                identifier: 'lastname',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your lastname'
                    },

                ]
            },


            Subject: {
                identifier: 'subject',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your subject'
                    },

                ]
            },

        }
    })
    ;


