// https://docs.cypress.io/api/table-of-contents

describe('Login Test', () => {
    const username = 'admin';
    const password = '123456';
    const wrongPassword = '666666';
    const wrongSidentifyCode = '1234';
    it('验证码错误', () => {
        cy.visit('/');
        //:nth-child(n)选择器，匹配其父元素的第n个子元素，不论类型，右键copy-copy selector
        cy.get('.login-input > div:nth-child(1) > input').type(username);
        cy.get('.login-input > div:nth-child(2) > input').type(password);
        cy.get('.sidentifyContent > div:nth-child(1) > input').type(
            wrongSidentifyCode,
        );
        cy.get('#login-btn').click();
        cy.contains('div', '验证码输入错误');
    });
    it('密码输入错误', () => {
        cy.visit('/');

        //:nth-child(n)选择器，匹配其父元素的第n个子元素，不论类型，右键copy-copy selector
        cy.get('.login-input > div:nth-child(1) > input').type(username);
        cy.window().then((win) => {
            const systemVerificationCode = win.Login.systemVerificationCode;
            cy.get('.login-input > div:nth-child(2) > input').type(
                wrongPassword,
            );
            cy.get('.sidentifyContent > div:nth-child(1) > input').type(
                systemVerificationCode,
            );
            cy.get('#login-btn').click();
            cy.contains('div', '账号或密码错误！');
        });
    });
    it('密码输入正确', () => {
        cy.visit('/');

        //:nth-child(n)选择器，匹配其父元素的第n个子元素，不论类型，右键copy-copy selector
        cy.get('.login-input > div:nth-child(1) > input').type(username);
        cy.window().then((win) => {
            const systemVerificationCode = win.Login.systemVerificationCode;
            cy.get('.login-input > div:nth-child(2) > input').type(password);
            cy.get('.sidentifyContent > div:nth-child(1) > input').type(
                systemVerificationCode,
            );
            cy.get('#login-btn').click();
            cy.contains('div', '登录成功');
        });
    });
});
