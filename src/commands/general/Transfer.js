const patron = require('patron.js');
const db = require('../../database');
const USD = require('../../utility/USD.js');
const Constants = require('../../utility/Constants.js');
const StringUtil = require('../../utility/StringUtil.js');

class Transfer extends patron.Command {
  constructor() {
    super({
      names: ['transfer', 'sauce', 'donate'],
      groupName: 'general',
      description: 'Transfer money to any member.',
      args: [
        new patron.Argument({
          name: 'member',
          key: 'member',
          type: 'member',
          example: '"Supa Hot Fire#1337"',
          preconditions: ['noself']
        }),
        new patron.Argument({
          name: 'transfer',
          key: 'transfer',
          type: 'quantity',
          example: '500',
          preconditions: ['cash', { name: 'minimumcash', options: { minimum: Constants.transfer.min } }]
        })
      ]
    });
  }

  async run(msg, args, sender) {
    const transactionFee = args.transfer * Constants.transfer.cut;
    const received = args.transfer - transactionFee;
    const newCash = await db.users.modifyCash(msg.member, -args.transfer);

    await db.users.modifyCash(args.member, received);
    return sender.reply('You have successfully transfered ' + USD(received) + ' to '+ StringUtil.boldify(args.member.user.tag) + '. Transaction fee: ' + USD(transactionFee) + '. Balance: ' + USD(newCash) + '.');
  }
}

module.exports = new Transfer();
