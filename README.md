
# IBLUEIT 5.0 FLOW PSICOFISIOLÓGICO 🐬

## Índice
1. [Visão Geral](#visão-geral)
2. [Resumo](#resumo)
3. [Características Principais](#características-principais)
4. [Instalação](#instalação)
5. [Como Jogar](#como-jogar)
6. [Tecnologias Utilizadas](#tecnologias-utilizadas)
7. [Contato](#contato)

## Visão Geral
"<b>I Blue It</b>" é um Serious Exergame para Reabilitação Respiratória. O jogador/paciente controla com a respiração o golfinho Blue em sua viagem, coletanto itens e desviando de obstáculos. 

# Público Alvo: 
Jogadores com enfermidades respiratórias (crianças e adultos).

# Funcionamento:
O jogo digital se comunica com um dispositivo similar a um pneumotacógrafo (PITACO) e um dispositivo similar a um Manovacuômetro (MANO-BD) que permitem a captura do processo respiratório do jogador. Os movimentos do Blue (o golfinho) são controlados em função das ações respiratórias (inspirar ou expirar) que o jogador fizer no dispositivo.

## Resumo
Jogos sérios são recursos complementares às modalidades terapêuticas tradicionais, promovendo exercícios que induzem demandas fisiológicas similares aos métodos convencionais, além de oferecer entretenimento e suporte adjuvante à reabilitação. Diante disso, destaca-se a necessidade de equilibrar os aspectos educacionais com a diversão em jogos sérios, controlando as ações de ajuste de dificuldade durante os exercícios fisioterapêuticos para evitar riscos de exaustão, que podem expor o paciente a perigos ao utilizar o jogo. Propõe-se a extensão da Teoria do Flow, que é peculiar aos jogos digitais, para o conceito Flow Psicofisiológico em jogos sérios multimodais destinados à reabilitação, que visa ajustar o esforço exigido à condição de saúde do paciente. Uma revisão da literatura revelou uma lacuna na abordagem de ajuste de dificuldade dos exergames conforme a condição de saúde do paciente utilizando Inteligência Artificial. O Flow Psicofisiológico foi implementado no serious exergame I Blue It para fisioterapia respiratória, após a coleta de requisitos para promover o treinamento muscular respiratório, por entrevista semiestruturada com 10 especialistas e análise de conteúdo. Foi utilizada o método de Ajuste Dinâmico de Dificuldade por meio de Aprendizado por Reforço e Redes Neurais Artificiais, intitulada DeepDDA. Os resultados obtidos, consideram múltiplas valências psicofisiológicas do paciente para promover segurança (análise dos biosinais), conforto físico (flow fisiológico), conforto motivacional (flow psíquico) e diversão (jogo digital). O agente DeepDDA utilizou a técnica Proximal Policy Optimization, onde o modelo aprendeu a balancear os desafios do exergame com as capacidades dos jogadores. O estado de Flow Psicofisiológico, evita cansaço ou sossego na dimensão do flow físico, evita frustração ou tédio na dimensão psíquica e, equilibra o objetivo sério da terapia com a diversão do exergame. Os resultados do Flow Psicofisiológico têm potencial de uso em exercícios terapêuticos em outras áreas da saúde. O conceito de Flow Psicofisiológico introduz o princípio do equilíbrio tridimensional entre os eixos de dificuldade, desempenho psíquico e desempenho terapêutico e, se beneficia de tecnologias multimodais, multidados e inteligência artificial.

## Características Principais
- **Conforto físico (flow fisiológico)**: Do ponto de vista psicológico, o jogo manterá o usuário engajado e motivado. Através da manipulação inteligente de elementos do jogo, com níveis progressivos de desafio, o DDA otimizará o flow psíquico, garantindo que o exercício seja percebido como divertido, recompensador e psicologicamente gratificante.
- **Conforto motivacional (flow psíquico)**: A dimensão fisiológica envolve o conforto físico do jogador, essencial para a adesão a longo prazo e efetividade do tratamento. O DDA ajustará os desafios do jogo para manter o usuário dentro de um estado de flow fisiológico, onde a atividade não é nem demasiadamente exigente nem insuficientemente estimulante, ajudando a maximizar a eficácia dos exercícios sem causar desconforto ou exaustão.
- **Segurança (análise dos biosinais)**: O DDA realizará o monitoramento para garantir a segurança durante o uso do jogo. Através da análise dos biosinais, como saturação de oxigênio, será possível verificar se o exercício está sendo realizado dentro dos limites seguros para o paciente. Este monitoramento protege o paciente contra riscos potenciais.
- **Diversão (jogo sério)**: Além de seus aspectos terapêuticos, um jogo digital é projetado para ser divertido e envolvente. A integração do conceito Flow Psicofisiológico visa reforçar a natureza lúdica do jogo, tornando-o uma ferramenta eficaz para reabilitação que simultaneamente entretém e auxilia na recuperação física. A diversão é um componente que pode transformar a percepção da terapia de uma tarefa árdua para uma atividade agradável.
- **Instrumento terapêutico tecnológico (Inteligência Artificial)**: O módulo DDA contribui como um instrumento de reabilitação (respiratória). Ao incorporar tecnologias de IA, o jogo oferece uma plataforma que adapta os exercícios às necessidades específicas de cada paciente, proporcionando uma abordagem terapêutica personalizada.

## 🔧 Instalação
• [Download do jogo](https://drive.google.com/file/d/1hMCceIuWfxUN4Lo2ZMm1Z0UdCUbbyTv7/view?usp=drive_link) 

## 🚀 Como Jogar
- Solicitação de cursos: iblueit.therapy@gmail.com

# Controles: 
Você usa a sua respiração como controle. 
Inspirar -> Sobe; 
Expirar -> Desce.

# Manuais de Construção dos dispositivos:
• [Manual de montagem do PITACO (Arduino Uno)](https://drive.google.com/file/d/1ySXKeuSn3YmyW2DLWHif1NNSO-Nl6EtK/view?usp=sharing) 
• [Manual de montagem do PITACO (Arduino Nano)](https://drive.google.com/file/d/1wr6Y98nJR3gZsatfQ0GP4eBEVyYnXiPZ/view?usp=sharing) 
• [Manual de montagem do MANO-BD (Arduino Uno)](https://drive.google.com/file/d/17r0CipR6f9x6s7APGRfAPqf73Xkfc3CW/view?usp=sharing) 

### ⚙️ Pré-requisitos
- [I Blue It Health InfoCharts](https://github.com/UDESC-LARVA/iblueit-health-Infocharts)
- [I Blue It Server Side](https://github.com/UDESC-LARVA/iblueit-server-side)
- [I Blue It Setup Prediction](https://github.com/UDESC-LARVA)

## Tecnologias Utilizadas
- [Unity](https://unity.com/) 2022.3.4f1
- [C#](https://docs.unity3d.com/ScriptReference/index.html)
- [ML-Agents](https://github.com/Unity-Technologies/ml-agents)
- [TensorFlow](https://www.tensorflow.org/)
- [PyTorch](https://pytorch.org/)

## 📌 Histórico de Versões

**Versão 4.0**
Projeto Multimodal: **[UDESC-LARVA/IBLUEIT](https://github.com/UDESC-LARVA/IBLUEIT)**

• Acesse o site: [I Blue It Health InfoCharts](https://www.iblueit.com.br)

## Contato
Informações de contato dos responsáveis pelo projeto: iblueit.therapy@gmail.com
