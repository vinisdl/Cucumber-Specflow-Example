#language: pt-BR
Funcionalidade: Entrar no google e pesquisar 

@google
Cenario: Pesquisar site do marco
	Dado Eu estou na home
	E digito no campo de pesquisa "marcodalalba"
	Quando Clico em pesquisar
	Entao Devo ver "marcodalalba"

