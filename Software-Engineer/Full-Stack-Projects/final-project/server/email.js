const nodemailer = require('nodemailer');

function sendEmail(item) {
    debugger
    const transporter = nodemailer.createTransport({
        service: 'outlook',
        auth: {
            user: item.managerEmail,
            pass: item.managerEmailPassword
        }
    });
  
    var mailOptions = {
        from: item.managerEmail,
        to: item.receiver,
        subject: item.subject,
        html: item.html
    };

    transporter.sendMail(mailOptions, function(error, info){
        if (error) {
          console.log(error);
        } else {
          console.log('Email sent: ' + info.response);
        }
      });

  }
  
  //babysittermanager@outlook.co.il
  //Baby1111
  

  module.exports={
    sendEmail
  }