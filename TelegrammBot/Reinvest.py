
class Reinvest:
    def __init__(self, chis, dni):

        self.chis = chis

        self.dni = dni
        self.mas = [0]

    def calc3(self):
        i = 0

        self.mas[0] = self.chis
        if self.mas[0] < 1000:
            proc = self.mas[0] * 0.009
        elif self.mas[0] < 10000:
            proc = self.mas[0] * 0.008
        elif self.mas[0] >= 10000:
            proc = self.mas[0] * 0.009
        n = 0
        proc
        while n <= self.dni:
            if self.mas[n] < 1000:
                self.mas.append(proc + self.mas[n])
            elif self.mas[n] < 10000:
                self.mas.append(proc + self.mas[n])
            elif self.mas[n] >= 10000:
                self.mas.append(proc + self.mas[n])
            n += 1

    def label3(self):
        k = 0

        s = ""
        while k <= self.dni:
            s += ("\n" + str(k) + " = " + str(self.mas[k]) + ";")
            k += 1
        return s


def calculateReinvest(days, pzm):
    data1 = Reinvest(pzm, days)
    data1.calc3()
    return data1.label3()
